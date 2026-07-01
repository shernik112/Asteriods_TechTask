using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.System;
using Project.UI;
using UnityEngine;

namespace Project.Enemies
{
    public class EnemyHitReaction 
    {
        private readonly HandlerScore _score;
        private readonly AudioHandler _audio;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly EnemyDefinition _enemyData;

        public EnemyHitReaction(
            HandlerScore score,
            AudioHandler audio,
            SpriteRenderer spriteRenderer,
            EnemyDefinition enemyData)
        {
            _score = score;
            _audio = audio;
            _spriteRenderer = spriteRenderer;
            _enemyData = enemyData;
        }

        public async UniTask PlayReaction(
            Action typeHit,
            CancellationToken ct)
        {
            _spriteRenderer.sprite = _enemyData.HitSprite;
            _audio.PlaySfx(_enemyData.HitClip);

            await UniTask.Delay(_enemyData.TimeHitReactionMs, cancellationToken: ct);

            _spriteRenderer.sprite = _enemyData.Sprite;
            _score.CountScoreDefeatedEnemy(_enemyData.ScoreByHit);
            typeHit?.Invoke();
        }
    }
}