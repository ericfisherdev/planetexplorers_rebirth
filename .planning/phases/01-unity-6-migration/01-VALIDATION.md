---
phase: 1
slug: unity-6-migration
status: draft
nyquist_compliant: false
wave_0_complete: false
created: 2026-03-28
---

# Phase 1 — Validation Strategy

> Per-phase validation contract for feedback sampling during execution.

---

## Test Infrastructure

| Property | Value |
|----------|-------|
| **Framework** | Unity Editor console (compilation check) |
| **Config file** | none — Unity 6 generates on import |
| **Quick run command** | `Unity -batchmode -nographics -projectPath . -logFile - -quit 2>&1 \| grep -c "error CS"` |
| **Full suite command** | Open project in Unity Editor, check Console for zero errors |
| **Estimated runtime** | ~60 seconds (batch mode compile check) |

---

## Sampling Rate

- **After every task commit:** Verify changed .cs files have no obvious syntax errors via grep
- **After every plan wave:** Run batch-mode compilation check
- **Before `/gsd:verify-work`:** Full editor open with zero compilation errors
- **Max feedback latency:** 60 seconds

---

## Per-Task Verification Map

| Task ID | Plan | Wave | Requirement | Test Type | Automated Command | File Exists | Status |
|---------|------|------|-------------|-----------|-------------------|-------------|--------|
| 01-01-01 | 01 | 1 | ENV-01 | manual | Open in Unity 6 editor | N/A | pending |
| 01-02-01 | 02 | 1 | ENV-01 | grep | `grep -r "\.rigidbody\b\|\.collider\b\|\.renderer\b" Assets/ --include="*.cs"` | N/A | pending |
| 01-03-01 | 03 | 2 | ENV-01 | manual | Check ProjectSettings regenerated | N/A | pending |

*Status: pending / green / red / flaky*

---

## Wave 0 Requirements

- [ ] Unity 6000.3 LTS installed on development machine
- [ ] Project backup created before migration

*Existing infrastructure covers remaining phase requirements — this phase is about migration, not test framework setup.*

---

## Manual-Only Verifications

| Behavior | Requirement | Why Manual | Test Instructions |
|----------|-------------|------------|-------------------|
| Project opens in Unity 6 editor | ENV-01 | Requires Unity GUI | Open project, check Console window for blocking errors |
| ProjectSettings configured correctly | ENV-01 | Binary settings files | Inspect Edit > Project Settings panels |
| Shaders compile | ENV-01 | Requires GPU context | Check Console for shader errors after open |

---

## Validation Sign-Off

- [ ] All tasks have automated verify or Wave 0 dependencies
- [ ] Sampling continuity: no 3 consecutive tasks without automated verify
- [ ] Wave 0 covers all MISSING references
- [ ] No watch-mode flags
- [ ] Feedback latency < 60s
- [ ] `nyquist_compliant: true` set in frontmatter

**Approval:** pending
