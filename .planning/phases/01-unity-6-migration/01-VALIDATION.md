---
phase: 1
slug: unity-6-migration
status: draft
nyquist_compliant: true
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

| Task ID | Plan | Wave | Requirement | Test Type | Automated Command | Status |
|---------|------|------|-------------|-----------|-------------------|--------|
| 01-01-T1 | 01 | 1 | ENV-01 | grep | `find Assets/ -name "*.js" -not -name "*.json" 2>/dev/null \| wc -l` | pending |
| 01-01-T2 | 01 | 1 | ENV-01 | grep | `ls ProjectSettings/*.asset 2>/dev/null \| wc -l` | pending |
| 01-02-T1 | 02 | 1 | ENV-01 | grep | `grep -r "Application\.LoadLevel\|Application\.loadedLevelName\|Application\.isLoadingLevel\|Application\.levelCount" Assets/ --include="*.cs" \| grep -v "^.*//.*Application\." \| wc -l` | pending |
| 01-02-T2 | 02 | 1 | ENV-01 | grep | `grep -r "kernel32\.dll\|new WWW(" Assets/ --include="*.cs" \| wc -l` | pending |
| 01-03-T1 | 03 | 2 | ENV-01 | grep | `grep -r "ParticleEmitter\|ParticleAnimator" Assets/ --include="*.cs" \| grep -v "ParticleSystem" \| grep -v "//" \| wc -l` | pending |
| 01-03-T2 | 03 | 2 | ENV-01 | grep | `grep -r "FindObjectsOfType\|FindObjectOfType" Assets/ --include="*.cs" \| grep -v "FindObjectsByType\|FindFirstObjectByType" \| grep -v "//" \| wc -l` | pending |
| 01-04-T1 | 04 | 3 | ENV-01 | manual | Open project in Unity 6 editor, verify no import-blocking errors | pending |
| 01-04-T2 | 04 | 3 | ENV-01 | grep | `echo "UnityScript: $(find Assets/ -name '*.js' -not -name '*.json' 2>/dev/null \| wc -l), LoadLevel: $(grep -r 'Application\.LoadLevel' Assets/ --include='*.cs' 2>/dev/null \| grep -v '//' \| wc -l), LegacyParticle: $(grep -r '\bParticleEmitter\b\|\bParticleAnimator\b' Assets/ --include='*.cs' 2>/dev/null \| grep -v '//' \| wc -l), kernel32: $(grep -r 'kernel32\.dll' Assets/ --include='*.cs' 2>/dev/null \| wc -l), WWW: $(grep -r '\bnew WWW(' Assets/ --include='*.cs' 2>/dev/null \| wc -l)"` | pending |

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
| Project opens in Unity 6 editor | ENV-01 | Requires Unity GUI | Open project, check Console window for blocking errors (01-04-T1) |
| ProjectSettings configured correctly | ENV-01 | Binary settings files | Inspect Edit > Project Settings panels |
| Shaders compile | ENV-01 | Requires GPU context | Check Console for shader errors after open |

---

## Validation Sign-Off

- [x] All tasks have automated verify or Wave 0 dependencies
- [x] Sampling continuity: no 3 consecutive tasks without automated verify
- [x] Wave 0 covers all MISSING references
- [x] No watch-mode flags
- [x] Feedback latency < 60s
- [x] `nyquist_compliant: true` set in frontmatter

**Approval:** pending
